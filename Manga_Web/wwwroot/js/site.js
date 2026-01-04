 // Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//deze code is voor de Nieuws te sorteren op datum
document.addEventListener('DOMContentLoaded', function () {
    //Nieuws sorteren
    const sorteerKnopNieuws = document.querySelector('.sorteren');
    const containerNieuws = document.querySelector('.Nieuws-container');
    if (sorteerKnopNieuws && containerNieuws) {
        let isDalend = true;
        sorteerKnopNieuws.addEventListener('click', function () {
            const cards = Array.from(containerNieuws.querySelectorAll('.Nieuws-card'));
            cards.sort((cardA, cardB) => {
                const dateA = new Date(cardA.dataset.date);
                const dateB = new Date(cardB.dataset.date);
                return isDalend ? dateB - dateA : dateA - dateB;
            });
            containerNieuws.innerHTML = '';
            cards.forEach(card => containerNieuws.appendChild(card));
            isDalend = !isDalend;
            sorteerKnopNieuws.textContent = isDalend
                ? 'Sorteer op datum (Nieuwste eerst)'
                : 'Sorteer op datum (Oudste eerst)';
        });
    }
});