btprint
=======

Windows Mobile BT direct print using socket

This is a compact framework Visual Studio 2008 SmartDevice project showing how to connect a socket to a paired/bonded BT device. You can then send and receive data to the BT device using a socket.

The project uses the long time ago published basic Windows Mobile BT wrapper, which is switched to the huge 32feet library. I choosed the basic lib and extended it lightly. The 32feet lib would be an overkill for this usage.

The socket send and receive functions are located in a separate thread and so will not block the main app.

keywords: Bluetooth, Compact Framework, Windows Mobile, socket, background thread, Bluetooth printing
