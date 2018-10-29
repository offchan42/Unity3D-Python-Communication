# Unity3D-Python-Communication

A very simple, fast, and general inter-process communication example between Unity3D C# and Python, using ZeroMQ.

## Introduction

* Have you ever tried to communicate C# code in Unity3D with Python before but could not find a satisfying solution?
* Have you ever tried implementing communication protocol using file read/write and found out that it's a stupid approach?
* Have you ever tried communicating using Web HTTP request and found out that it's stupidly slow and high latency?
* Have you ever tried communicating using socket stuff, but it feels like you are reinventing the wheel?
* Have you ever tried to communicate by emulating a serial port, and found out that it's not how cool guys do work?
* Have you ever tried to send Unity input to python and do some scientific work (maybe even machine learning task)
  and return the output to Unity?
* Have you ever tried to build a `.dll` from python or even rewrite everything in `C#` because you don't know how to
  communicate between python and C# processes?
* Have you ever tried to export a `TensorFlow Protobuf Graph` (Deep learning model) and use `TensorFlowSharp` or
  `OpenCVForUnity` to import the graph inside Unity because you want to use the model to predict stuff in Unity, but
  you don't know how to predict inside Python and send result to Unity, so you need to predict inside Unity instead?
  The heck, you even tried to learn to use `MLAgents` to solve this particular problem?

If you answer **Yes** to any of these questions but it seems you have found no solutions,
then this repository is definitely for you!
(If you answered **Yes to all** questions, you and me are brothers!)

I've tried a lot. With a lot of searching on the internet, I've found no solutions that is simple, fast, and general
enough that I can apply to any kind of communication between Python and Unity3D. All I've done in the past were simply
a hack to either get my scientific computation work in Unity instead of python, or communicate between the processes painfully.

_Until I found ZeroMQ approach from this [repository](https://github.com/valkjsaaa/Unity-ZeroMQ-Example)
(and some head scratching)._

## Solution Explanation

I've built a `request-reply` model of ZeroMQ where Python (server) replies whenever Unity (client) requests
a service from Python.

Most of the code are just copies from the official tutorial. I try to make this as simple to grasp as possible, so I
only log the message to the console and nothing else.

## Getting Started

1. Open UnityProject (its `dll` files are using .NET 4.x version), run the `SampleScene`.
2. Run python file `server.py` using command `python server.py` on a command prompt.
3. You should start seeing messages being logged inside Unity and the command prompt.

Specifically, Unity will send request with a message `Hello` 10 times, and Python will simply reply `World` 10 times.
There is a one second sleep between each reply on the server (to simulate long processing time of the request).

Please read the comments inside python files and you will understand everything more deeply.

**The most important thing is that you should follow the 3 getting started steps first. Don't skip it!**

After you've understood most of the stuff but it's not advanced enough, you should consult the official
[ØMQ - The Guide](http://zguide.zeromq.org/page:all).

## Requirements

* [ZeroMQ](http://zeromq.org/) is a very fast messaging library and it's simple enough that a few lines of code works.
* [PyZMQ](https://pyzmq.readthedocs.io/en/latest/) is the Python bindings for ZeroMQ. You can install it using
  `pip install pyzmq` command or see more installation options [here](http://zeromq.org/bindings:python) or
  [here](https://github.com/zeromq/pyzmq).
* [NetMQ](https://netmq.readthedocs.io/en/latest/) is a native C# port of ZeroMQ. Normally you need to install this using
  `NuGet` package manager inside `Visual Studio` when you want to build a .NET application, or you could install using
  `.NET CLI`. But for this repository here, you don't need to do any of the installation because we've already included
  `AsyncIO.dll` and `NetMQ.dll` for you inside `Assets/Plugins/` directory.
  If you want to build your own `dll` files, please take a look at
  [this issue](https://github.com/valkjsaaa/Unity-ZeroMQ-Example/issues/7).