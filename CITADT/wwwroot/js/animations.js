// Initialize AOS with better performance settings
document.addEventListener('DOMContentLoaded', function() {
    AOS.init({
        duration: 800,
        once: true,
        disable: 'mobile',
        startEvent: 'DOMContentLoaded',
        offset: 50,
        delay: 0,
        easing: 'ease-in-out',
        mirror: false,
        anchorPlacement: 'top-bottom'
    });

    // Use event delegation for better performance
    document.addEventListener('mouseover', function(e) {
        // Card hover effect
        if (e.target.closest('.card')) {
            const card = e.target.closest('.card');
            card.style.transform = 'translateY(-5px)';
            card.style.transition = 'transform 0.3s ease';
        }
        
        // Table row hover effect
        if (e.target.closest('tbody tr')) {
            const row = e.target.closest('tbody tr');
            row.style.backgroundColor = '#f8f9fa';
            row.style.transition = 'background-color 0.3s ease';
        }
    });

    document.addEventListener('mouseout', function(e) {
        // Card hover effect
        if (e.target.closest('.card')) {
            const card = e.target.closest('.card');
            card.style.transform = 'translateY(0)';
        }
        
        // Table row hover effect
        if (e.target.closest('tbody tr')) {
            const row = e.target.closest('tbody tr');
            row.style.backgroundColor = '';
        }
    });

    // Optimized ripple effect
    document.addEventListener('click', function(e) {
        const button = e.target.closest('.btn');
        if (!button) return;

        const ripple = document.createElement('span');
        ripple.classList.add('ripple');
        
        const rect = button.getBoundingClientRect();
        const size = Math.max(rect.width, rect.height);
        
        ripple.style.width = ripple.style.height = `${size}px`;
        ripple.style.left = `${e.clientX - rect.left - size/2}px`;
        ripple.style.top = `${e.clientY - rect.top - size/2}px`;
        
        button.appendChild(ripple);
        
        // Remove ripple after animation
        ripple.addEventListener('animationend', () => {
            ripple.remove();
        });
    });

    // Add smooth scroll behavior
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Handle mobile menu toggle
    const menuToggle = document.querySelector('[data-bs-toggle="collapse"]');
    if (menuToggle) {
        menuToggle.addEventListener('click', function() {
            const target = document.querySelector(this.dataset.bsTarget);
            if (target) {
                target.classList.toggle('show');
            }
        });
    }

    // Staggered animation for login form elements
    const animatedElements = document.querySelectorAll('.animate-fade-in');
    
    animatedElements.forEach((el, index) => {
        el.style.animationDelay = `${0.2 * (index + 1)}s`;
    });

    // Form validation visual feedback
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        const inputs = loginForm.querySelectorAll('input[required]');
        
        inputs.forEach(input => {
            // Add validation styling
            input.addEventListener('blur', function() {
                if (this.value.trim() === '') {
                    this.classList.add('border-red-500');
                    this.classList.remove('border-green-500');
                } else {
                    this.classList.remove('border-red-500');
                    this.classList.add('border-green-500');
                }
            });
            
            // Remove validation styling on focus
            input.addEventListener('focus', function() {
                this.classList.remove('border-red-500');
            });
        });
    }

    // Button effect
    const buttons = document.querySelectorAll('button');
    buttons.forEach(button => {
        button.addEventListener('mousedown', function() {
            this.classList.add('scale-95');
        });
        
        button.addEventListener('mouseup', function() {
            this.classList.remove('scale-95');
        });
        
        button.addEventListener('mouseleave', function() {
            this.classList.remove('scale-95');
        });
    });
}); 