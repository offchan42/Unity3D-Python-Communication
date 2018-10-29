# Unity3D-Python-Communication

A very simple, fast, and general inter-process communication example between Unity3D C# and Python, using ZeroMQ.

## Introduction

* Have you ever wanted to send Unity input to python and do some scientific work (maybe even machine learning task)
  and return the output to Unity?
* Have you ever tried to communicate C# code in Unity3D with Python before but seems like there are no reliable solutions?
* Have you ever tried implementing communication protocol using file read/write and found out that it's a stupid approach?
* Have you ever tried communicating using Web HTTP request and found out that it's stupidly slow and high latency?
* Have you ever tried to communicate by emulating a serial port, and found out that it's not how cool guys do work?

If you answer **Yes** to any of these questions but it seems you have found no solutions,
then this repository is definitely for you!
(If you answered **Yes to all** questions, you and me are brothers!)

I've tried a lot. With a lot of searching on the internet, I've found no solutions that is simple, fast, and general
enough that I can apply to any kind of communication between Python and Unity3D.

_Until I found ZeroMQ and NetMQ approach (and some head scratching)._

ZeroMQ is a very fast messaging approach and it's simple enough that a few lines of code works.

NetMQ is a C# implementation of ZeroMQ.