const WebSocket = require('ws')

wss = new WebSocket.Server({port: 8080}, () => {
    console.log('Server successfully initialized')
})

wss.on('connection', (ws) => {
    ws.on('message', (data) => {
        ws.send(data)
    })
})

wss.on('listening', () => {
    console.log("Server is listening on port 8080")
})