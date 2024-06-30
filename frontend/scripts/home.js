document.addEventListener('DOMContentLoaded', async function() {
    const statusDiv = document.getElementById('status');
    const token = getCookie('jwtToken');

    if (!token) {
        statusDiv.innerHTML = '<p>Você precisa fazer login primeiro!</p>';
        return;
    }

    try {
        const response = await fetch("https://localhost:7117/api/home/health", {
            method: 'GET',
            headers: { 'Authorization': `Bearer ${token}` }
        });

        const data = await response.json();

        if (response.ok && data.statusCode === 200) {
            statusDiv.innerHTML = '<p>Você está logado! Tudo está ok.</p>';
        } else {
            const errorMessage = data.errors && data.errors.length > 0 ? data.errors[0].errorMessage : "Erro desconhecido";
            statusDiv.innerHTML = `<p>Erro ao verificar status: ${errorMessage} (Status: ${data.statusCode})</p>`;
        }
    } catch (error) {
        console.error("Erro:", error);
        statusDiv.innerHTML = '<p>Erro ao verificar status! Verifique a conexão e tente novamente.</p>';
    }
});

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}
