// SignalR client: keeps the horse list live across tabs and shows a toast per change.
// Method names must match HubContract in HorseClub.Services.KhoiND.Realtime.
(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/horseclub")
        .withAutomaticReconnect()
        .build();

    function showToast(message) {
        const container = document.getElementById("toast-container");
        if (!container) return;

        const toast = document.createElement("div");
        toast.className = "toast align-items-center text-bg-dark border-0";
        toast.setAttribute("role", "status");
        toast.innerHTML =
            '<div class="d-flex"><div class="toast-body"></div>' +
            '<button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button></div>';
        toast.querySelector(".toast-body").textContent = message;   // never inject HTML from the wire
        container.appendChild(toast);
        toast.addEventListener("hidden.bs.toast", () => toast.remove());
        new bootstrap.Toast(toast, { delay: 4000 }).show();
    }

    // Re-fetch the current page (keeps the search query) and swap only the list.
    async function refreshList() {
        const list = document.getElementById("horse-list");
        if (!list) return;

        const response = await fetch(window.location.href, { headers: { "X-Requested-With": "fetch" } });
        if (!response.ok) return;
        const html = new DOMParser().parseFromString(await response.text(), "text/html");
        const fresh = html.getElementById("horse-list");
        if (fresh) list.replaceWith(fresh);
    }

    connection.on("ItemChanged", async (action, item) => {
        showToast(`Horse "${item.name}" was ${action}.`);
        await refreshList();
    });

    connection.on("Notification", (message) => showToast(message));

    connection.start().catch((err) => console.error("SignalR connection failed:", err));
})();
