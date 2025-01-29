const modals = document.querySelectorAll('.custom-modal');
const closeButtons = document.querySelectorAll('.custom-modal-close');
const navigationBar = document.getElementById('navBar');

function openModal(modal) {
    modal.style.display = 'flex';
    navigationBar.style.display = 'none';
}

function closeModal(modal) {
    modal.style.display = 'none';
    navigationBar.style.display = 'block';
}

closeButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modal = button.closest('.custom-modal');
        closeModal(modal);
    });
});

window.addEventListener('click', (event) => {
    modals.forEach(modal => {
        if (event.target === modal) {
            closeModal(modal);
        }
    });
});

const profileNav = document.getElementById('profileNav');
const profileModal = document.getElementById('profileModal');

profileNav.addEventListener('click', (event) => {
    event.preventDefault();
    openModal(profileModal);
});

const newsModal = document.getElementById('newsModal');
const moreAboutLinks = document.querySelectorAll(".more-about-link");

moreAboutLinks.forEach(link => {
    link.addEventListener('click', (event) => {
        event.preventDefault();
        openModal(newsModal);
    });
});

// Link Discussion button to Course Discussion modal
const discussionButton = document.getElementById('discussionButton');
const courseModal = document.getElementById('courseModal');

discussionButton.addEventListener('click', () => {
    openModal(courseModal);
});
