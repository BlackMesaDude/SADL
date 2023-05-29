# SADL <img src="https://img.shields.io/badge/Version-1.0-informational" /> <img src="https://img.shields.io/badge/License-GPL--3.0-informational" /> <img src="https://github.com/francescomesianodev/SADL/workflows/.NET%20Core/badge.svg" />

## Index

<!--ts-->
   * [SADL](#SADL)
      * [Features](#features)
   * [How does it work?](#how-does-it-work)
      * [AES](#aes)
      * [Jenkins](#jenkins)
   * [Support](#support)
   * [Notes](#notes)
<!--te-->

**SADL** is a library written in pure C# that allows to access protected game files from the *Rockstar Games*' title **Grand Theft Auto V**.

This library is a re-adaptation of the *GTA Toolkit* made by [Neodymium146](https://github.com/Neodymium146) back in 2015.

## Features

**SADL** gives the opportunity to execute r\w operation on the game's files using standardized and optimized methodologies that the *GTA Toolkit* doesn't have making the library affidable for future usages such as the creation of visual tools.

# How does it work?

The latest Grand Theft Auto use [AES](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard) encryption algorithm along with [Jenkins](https://en.wikipedia.org/wiki/Jenkins_hash_function) to protect any type of access into the game files.

## AES

**Advanced Encryption Standard** (or *AES*) is an algorithm that uses temporary matrice buffers to shift and mix the data accordingly.

The process starts by defining a 128-bit buffer where each byte will be combined accordingly in a bitwise xor manner. Additionally, the buffer is even more combined after a certain amount of steps using a look-up table and cyclical mixing the buffer even more. 

## Jenkins

The Jenkins Functions are non-cryptographic multiple hashing functions for multi-byte keys. In-short, they generate an hash based on a simple shifting on the original key.

Grand Theft Auto V uses Jenkins to define the **session key** that will be used with [AES](#aes).

# Support

Altrough the library is meant for `Windows x64` you could also use it for other versions from different platforms.

### Notes

> A warm thanks should go to [rickycorte](https://github.com/rickycorte) and [ItzTravelInTime](https://github.com/ItzTravelInTime) whom helped trough the development making this project more sustainable and optimized.
