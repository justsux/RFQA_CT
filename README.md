# RFQA Connection Tester
This tool can be helpful when you are trying to figure out your level of connection with Rainforest' Terminals.

Basically, it's a simple cmd wrapper so people can use commands like ping and tracert without getting a headache. Simply and straightforwardly by pressing one of the buttons.

![screen](http://rnfrst-forum-uploads.s3.amazonaws.com/original/1X/eedd4b6b79cbf6af9441b199a8da5f473d978600.png)


Below is a short explanation of the features:

1. **FULL SCAN** - scans the list of RF terminals (up to 125)  to judge if you are having an issue with the online terminal  or not.

2. **PING** - pings the chosen terminal ten times so you can see the min/max/average ping and the number of lost packets. The lower number - the better. Numbers to consider as relevant is anything from 5ms to 400ms (5-50ms - excellent, 51-100ms - great, 101-200 - okay, 201-300 - fair, 301-400 still legit). Anything higher than that can cause a huge drop in packets which can make testing a living hell.

3. **TRACE** - traces the route between you and the terminal and checks the number of hops. The fewer hops you have - the better. It will visualize the whole path so you can see where exactly is your problem comes from. Route example: IP of your router -> IP of your ISP router(s) -> IP from some random country(s) -> RF terminal.

4. **Custom domain/IP** field. Just put any other address that you want to test. For example, google's anycast 8.8.8.8 and check you ping with their closest public DNS. You can also put rainforestqa.com and see what is your current connection with the portal/forum/site. Could be useful when you are having an issue creating a new thread on the forum. To make sure that the issue is actually on RF's side and not because of your own connection.

5. **Terminal selector**. Pick a terminal to test and press ping/trace button to see the results.

### How To Use It

First press the **FULL SCAN** button, to see how the whole network looks from your side. Next...

I bet all of us seen some errors while using the testing client. One of those is the **Error #2048**.


![screen](http://rnfrst-forum-uploads.s3.amazonaws.com/original/1X/2b7b2cdfdfd83584f28954f0f0ec59d2ea49cb28.png)

On the screenshot above you may notice that one is trying to connect to the VM #20 (020).

After getting the basic idea of how the whole network looks like from your side, you want to pick the VM you want to test from the list (e.g. @rf_VM [020]) and then press the **PING** button to check the number of lost packets and your average ping with the VM.

![screen](http://rnfrst-forum-uploads.s3.amazonaws.com/original/1X/563713d58be7cb245d197126325cc29b6ec9b1b4.png)


If results of the ping command aren't great, you want to trace the route by pressing the **TRACE** button.
Numbers inside rectangles represent the number of hops. In this particular example, the VM was reached in 8 hops. Remember: The fewer hops - the better.

![screen](http://rnfrst-forum-uploads.s3.amazonaws.com/original/1X/155f7dcd646990ffff2a7befcb9b4b97e577f49c.png)

DOWNLOAD: [CLICK](https://github.com/justsux/RFQA_CT/releases/tag/v)

REQUIRES: [Net Framework 4.0+](https://www.microsoft.com/en-us/download/search.aspx?q=.net%20framework)
