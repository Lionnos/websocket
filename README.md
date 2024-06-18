<h1 align="center">Websocket y Socket</h1>

- Native .NET 8 websocket implementation, without SignalR
- TCP socket implementation for sending data in .NET 8
 
## Websocket
1. **Clone the Repository**
   ```bash
   https://github.com/Lionnos/websocket.git
   ```
2. **Run the "sln" in Visual Studio by going to the websocket folder**
   ```bash
   cd websocket/websocket
   ```

3. **Run the project with http configuration**
    ```bash
   chat.js
   ```
    If you run the project with https configuration, you will have to change the execution port. In the previous file found in view.

4. **Once the project is running, go to the view folder in websocket**
   ```bash
   cd view
   ```

5. **Run the view.html file**
   
    Once the html is running, enter the username to connect to the chat.

    Note: You can open other html windows to simulate the chat.

## Socket
1. **Go to the socket folder once the repository is cloned**
   ```bash
   cd websocket/socket
   ```

3. **Run the "sln" in Visual Studio**

    The socket is implemented as a client-server so that it works first run the "server" solution and then "client", you can run as many clients as you want.
