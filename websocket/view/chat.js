let username = '';
let usernameColor = getRandomColor();

const socket = new WebSocket("ws://localhost:5090/ws");

socket.onopen = function (e) {
    console.log("[open] Connection established");
    setUsername();
};

socket.onmessage = function (event) {
    let messagesDiv = document.getElementById('messages');
    let messageData = event.data.split(': ');
    let user = messageData[0];
    let message = messageData.slice(1).join(': ');
    let userColor = getUsernameColor(user);
    messagesDiv.innerHTML += `<p class="message"><span class="username" style="color: ${userColor};">${user}</span>: ${message}</p>`;
    messagesDiv.scrollTop = messagesDiv.scrollHeight;
};

socket.onclose = function (event) {
    if (event.wasClean) {
        console.log(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
    } else {
        console.log('[close] Connection died');
    }
};

socket.onerror = function (error) {
    console.log(`[error] ${error.message}`);
};

function setUsername() {
    username = prompt("Ingrese tu usuario");
    if (username.trim() === "") {
        setUsername();
        return;
    }
    document.getElementById('usernameDisplay').innerText = username;
}

function sendMessage() {
    const input = document.getElementById('messageInput');
    if (input.value.trim() === "") return;
    const message = `${username}: ${input.value}`;
    socket.send(message);
    input.value = '';
}

document.getElementById('messageInput').addEventListener('keyup', function (event) {
    if (event.key === 'Enter') {
        sendMessage();
    }
});

function getUsernameColor(user) {
    if (userColors.hasOwnProperty(user)) {
        return userColors[user];
    } else {
        userColors[user] = getRandomColor();
        return userColors[user];
    }
}

let userColors = {};

function getRandomColor() {
    let letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}
