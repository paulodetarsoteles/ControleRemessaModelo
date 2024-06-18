document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault(); // Evita o envio padrão do formulário

    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;

    // Cria uma nova instância de XMLHttpRequest
    var xhr = new XMLHttpRequest();

    // Configura a requisição para o método POST e o endpoint de login
    xhr.open("POST", "http://localhost:5152/api/home/login", true);

    // Define o header Content-Type
    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    // Define a função a ser executada quando a resposta for recebida
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                // Sucesso na resposta do servidor
                var response = JSON.parse(xhr.responseText);
                var token = response.token;

                alert("Login bem-sucedido!");

                // Aqui você pode salvar o token para uso futuro
                localStorage.setItem('jwtToken', token);
            } else {
                // Erro na resposta do servidor
                console.error("Erro: ", xhr.statusText);
                alert("Falha no login!");
            }
        }
    };

    // Prepara o corpo da requisição com as credenciais do usuário
    var data = JSON.stringify({
        UserName: username,
        Password: password
    });

    // Envia a requisição com o corpo
    xhr.send(data);
});

// Exemplo de função para fazer uma requisição autenticada
function fetchData() {
    var token = localStorage.getItem('jwtToken');

    if (!token) {
        alert("Você precisa fazer login primeiro!");
        return;
    }

    var xhr = new XMLHttpRequest();
    xhr.open("GET", "http://localhost:5152/api/home/index", true);
    xhr.setRequestHeader("Authorization", "Bearer " + token);

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                console.log("Dados recebidos: ", xhr.responseText);
            } else {
                console.error("Erro: ", xhr.statusText);
            }
        }
    };

    xhr.send();
}
