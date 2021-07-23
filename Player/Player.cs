using Godot;
using System;

using WebSocketSharp; // Useful : Getting websocket from nodejs server
using Newtonsoft.Json; // Useful : Converting json data to class objects


public class Player : KinematicBody2D
{
    [Export]
    public int moveSpeed = 250;
    
    WebSocket ws = new WebSocket("ws://localhost:8080");
    public override void _Ready()
    {
       
       ws.OnMessage += (sender, e) => {
        //    Console.WriteLine("Message received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        PlayerPos pos = JsonConvert.DeserializeObject<PlayerPos>(e.Data);
        GD.Print(pos.x); // Debugging X position from the player [Bounced from the server]

       };
       ws.Connect();
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 motion = new Vector2();
        motion.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        motion.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        
        // C# socket io test
        if(ws == null) return;
        ws.Send($"{{\"x\":{this.GlobalPosition.x}, \"y\":{this.GlobalPosition.y}}}");
        



        MoveAndCollide(motion.Normalized() * moveSpeed * delta);
    }
}

class PlayerPos
{
    public String x {get; set;}
    public String y {get; set;}
}