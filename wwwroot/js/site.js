// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// codigo do carrosel ========================================================================================================

let currentSlide = 0; // Variável para rastrear o slide atual
const slides = document.querySelectorAll('.carousel-images img'); // Seleciona todas as imagens do carrossel

// Função para mostrar o slide com base no índice
function showSlide(index) {
    slides.forEach((slide, i) => {
        slide.classList.remove('active'); // Remove a classe 'active' de todas as imagens
        if (i === index) {
            slide.classList.add('active'); // Adiciona a classe 'active' à imagem atual
        }
    });
}

// Função para mover o slide baseado na direção (1 para próximo, -1 para anterior)
function moveSlide(direction) {
    currentSlide += direction; // Atualiza o índice do slide atual

    // Verifica se o índice está fora dos limites
    if (currentSlide < 0) {
        currentSlide = slides.length - 1; // Volta para o último slide se estiver no início
    } else if (currentSlide >= slides.length) {
        currentSlide = 0; // Volta para o primeiro slide se estiver no final
    }

    showSlide(currentSlide); // Atualiza a exibição do slide
}

showSlide(currentSlide); // Inicializa o carrossel mostrando o primeiro slide


//area do cliente ===============================================================================================
// Função para abrir o modal
const modal = document.getElementById('clienteModal');
const areaClienteBtn = document.getElementById('areaClienteBtn');
const closeModal = document.getElementById('closeModal');

areaClienteBtn.addEventListener('click', function () {
    modal.style.display = 'flex'; // Mostra o modal
});

closeModal.addEventListener('click', function () {
    modal.style.display = 'none'; // Fecha o modal
});

// Fechar o modal ao clicar fora da área de conteúdo
window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = 'none';
    }
};

// Manipulação de exibição dos formulários de login e cadastro
const loginOption = document.getElementById('loginOption');
const cadastroOption = document.getElementById('cadastroOption');
const loginForm = document.getElementById('loginForm');
const cadastroForm = document.getElementById('cadastroForm');

loginOption.addEventListener('click', function () {
    loginForm.style.display = 'block'; // Mostrar formulário de login
    cadastroForm.style.display = 'none'; // Esconder formulário de cadastro
});

cadastroOption.addEventListener('click', function () {
    cadastroForm.style.display = 'block'; // Mostrar formulário de cadastro
    loginForm.style.display = 'none'; // Esconder formulário de login
});

// Função de envio de login (apenas para demonstração)
document.getElementById('submitLogin').addEventListener('click', function () {
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;
    console.log('Login:', { email, password });
    modal.style.display = 'none'; // Fecha o modal
});

// Função de envio de cadastro (apenas para demonstração)
document.getElementById('submitRegister').addEventListener('click', function () {
    const fullName = document.getElementById('fullName').value;
    const address = document.getElementById('address').value;
    const email = document.getElementById('registerEmail').value;
    const password = document.getElementById('registerPassword').value;
    console.log('Cadastro:', { fullName, address, email, password });
    modal.style.display = 'none'; // Fecha o modal
});
document.getElementById('areaClienteBtn').addEventListener('click', function () {
    document.querySelector('.modal').style.display = 'flex';
    document.body.classList.add('modal-open'); // Impede o scroll da página por trás
});

document.querySelector('.close-btn').addEventListener('click', function () {
    document.querySelector('.modal').style.display = 'none';
    document.body.classList.remove('modal-open'); // Volta o scroll
});

document.getElementById('areaClienteBtn').addEventListener('click', function() {
    document.getElementById('clienteModal').style.display = 'block';
});

document.getElementById('closeModal').addEventListener('click', function() {
    document.getElementById('clienteModal').style.display = 'none';
});

// Fechar modal se clicar fora
window.onclick = function(event) {
    if (event.target == document.getElementById('clienteModal')) {
        document.getElementById('clienteModal').style.display = 'none';
    }
};
