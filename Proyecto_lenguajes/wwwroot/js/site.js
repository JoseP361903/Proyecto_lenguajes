// Get elements
const profileModal = document.getElementById('profileModal'); // Modal element
const closeModal = document.querySelector('.custom-modal-close'); // Close button
const profileNav = document.getElementById('profileNav'); // Profile link in navbar
const navigationBar = document.getElementById('navBar');
const modal = document.getElementById("newsModal");
const moreAboutLinks = document.querySelectorAll(".more-about-link");



// Open modal when Profile is clicked
profileNav.addEventListener('click', (event) => {
    event.preventDefault(); // Prevent default link behavior
    profileModal.style.display = 'flex'; // Show the modal
    navigationBar.style.display = 'none';

});

// Close modal when the close button is clicked
closeModal.addEventListener('click', () => {
    profileModal.style.display = 'none'; // Hide the modal
    navigationBar.style.display = 'block';
});

// Close modal when clicking outside the modal content
window.addEventListener('click', (event) => {
    if (event.target === profileModal) {
        profileModal.style.display = 'none';
        navigationBar.style.display = 'block';
    }
});


    function openModal(event) {
        event.preventDefault(); 
        modal.style.display = "block";
        navigationBar.style.display = 'none';

    }

    moreAboutLinks.forEach(link => {
        link.addEventListener("click", openModal);

    });

