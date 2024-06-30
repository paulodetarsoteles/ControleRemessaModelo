document.getElementById('loginForm').addEventListener('submit', async function(event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    if (!username || !password) {
        alert("Username e Password são obrigatórios!");
        return;
    }

    try {
        const response = await fetch("https://localhost:7117/api/home/login", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ UserName: username, Password: password })
        });

        const data = await response.json();

        if (response.ok && data.success) {
            const token = data.bodyResponse[0].token; 

            alert("Login bem-sucedido!");

            setCookie('jwtToken', token, 1); // Exemplo: 1 dia de validade

            window.location.href = 'pages/home.html';
        } else {
            const errorMessage = data.errors && data.errors.length > 0 ? data.errors[0].errorMessage : "Erro desconhecido";
            alert(`Falha no login: ${errorMessage} (Status: ${data.statusCode})`);
        }
    } catch (error) {
        console.error("Erro:", error);
        alert("Erro no login! Verifique a conexão e tente novamente.");
    }
});

function setCookie(name, value, days) {
    const date = new Date();
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    const expires = `expires=${date.toUTCString()}`;
    const cookieOptions = `${name}=${value}; ${expires}; Secure; SameSite=Strict`;
    document.cookie = cookieOptions;
}
