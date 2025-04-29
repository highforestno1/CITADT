
function handleStickyHeader() {
    const header = document.querySelector('header');
    const topBar = document.querySelector('.bg-primary');

    if (!header || !topBar) return;

    const topBarHeight = topBar.offsetHeight;

    window.addEventListener('scroll', () => {
        if (window.scrollY > topBarHeight) {
            header.classList.add('fixed', 'top-0', 'left-0', 'right-0', 'z-50', 'shadow-md', 'animate-fadeIn');
        } else {
            header.classList.remove('fixed', 'top-0', 'left-0', 'right-0', 'z-50', 'shadow-md', 'animate-fadeIn');
        }
    });
}

// Function to handle mobile menu
function handleMobileMenu() {
    const mobileMenuButton = document.querySelector('[x-data]');
    const mobileMenu = document.getElementById('mobile-menu');

    if (!mobileMenuButton || !mobileMenu) return;

    document.addEventListener('click', (event) => {
        if (!mobileMenuButton.contains(event.target) && !mobileMenu.contains(event.target)) {
            mobileMenu.classList.add('hidden');
        }
    });
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    handleStickyHeader();
    handleMobileMenu();
});


// language list
function toggleDropdown() {
    const dropdown = document.getElementById('language-dropdown');
    dropdown.classList.toggle('hidden');
}

function selectLanguage(language) {
    document.getElementById('selected-language').textContent = language;
    toggleDropdown(); // Ẩn dropdown sau khi chọn
}

// Đóng dropdown khi click bên ngoài
document.addEventListener('click', function (event) {
    const dropdown = document.getElementById('language-dropdown');
    const button = document.querySelector('button');
    if (!dropdown.contains(event.target) && !button.contains(event.target)) {
        dropdown.classList.add('hidden');
    }
});

// Initialize Swiper

var swiper = new Swiper(".mySwiper", {
    slidesPerView: 1,
    autoplay: {
        delay: 3000,
        disableOnInteraction: false,
    },
    spaceBetween: 30,
    loop: true,
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});


