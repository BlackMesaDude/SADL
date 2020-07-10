# SADL

Index
=================

<!--ts-->
   * [Introduction](#SADL)
      * [Notes](#notes)
      * [Special Thanks](#special-thanks)
      * [What does it provide?](#what-does-it-provide)
   * [How does it work?](#how-does-it-work)
      * [AES](#aes)
      * [Jenkins](#jenkins)
   * [Support](#support)
   * [Repository Status](#repository-status)
<!--te-->

**SADL** is a library written in C#.net for **Grand Theft Auto V** a game made by the *Rockstar Games* developers. With this library the developer is able to encrypt or decrypt any supported resource that resides in the game files. 

This library is derived from the *GTA Toolkit* made by [Neodymium146](https://github.com/Neodymium146) back in 2015 that currently is taking dust. The library was mostly rewritten and optimized.

### Notes

**SADL** gives the opportunity to access game files and modify them as you want leading to a easy solution for modding game files. This library was meant to _re-port_ and optimize the known ways to access and edit game files, this library isn't meant for online modding and shouldn't be referenced as one. **Grand Theft Auto Online** is already a place destroyed by money and modders and **SADL** shouldn't be taken as a reason to it.

### Special Thanks

Thanks to [rickycorte](https://github.com/rickycorte) and [ItzTravelInTime](https://github.com/ItzTravelInTime) for helping me through the development of this library, leading to make this library more affidable and optimized for the next developer who will use it.

### What does it provide?

This library is splitted in two parts `RAGE` and `GrandTheftAuto`. **RAGE** provides a solution to accessing and decrypting, an example should be the Cryptography side where it offers the algorithm and other utilities to encrypt or decrypt for the specified security algorithm used by the game to the files. **GrandTheftAuto** provides the shortcuts to access and wrap the game files.

# How does it work?

Grand Theft Auto uses [AES](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard) encryption algorithm and [Jenkins](https://en.wikipedia.org/wiki/Jenkins_hash_function) hash function. These two have the scope to protect and serve a key to access the files.

### AES

AES is an encryption algorithm that uses byte matrices wich in the library can be found as **DataBlock(s)**, these will store the bytes of data that later will be processed and protected. 

The process starts with the `SubBytes` wich substitutes, in a non-linear way, every byte based on the DataBlock where it resides.   
`ShiftRows` takes place and allows to move the bytes to a n position based on the starting row. 
`MixColumns` combines the bytes with a linear operation, the bytes will be taken one column per time.
then `AddRoundKey` will combine, again, each byte of the DataBlock to the session key.

### Jenkins

Jenkins function is a non-cryptographic hash function for multi-byte keys. In short words the hash will be generated shifting the original key. This will be the **session key** mentioned in the [AES](#aes) section. 

### Support

| Platform 	| Support           	|
|----------	|-------------------	|
| Windows  	| x64 Only          	|
| Linux    	| x64 Only          	|
| MacOS    	| x64 BootCamp Only 	|

## Repository Status

![.NET Core Build](https://github.com/francescomesianodev/SADL/workflows/.NET%20Core/badge.svg) ![.NET Core Release](https://github.com/francescomesianodev/SADL/workflows/.NET%20Core/badge.svg?event=release)
