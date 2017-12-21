#!/bin/bash
#Logon script to assign smb network shares

#mkdir /Users/yetti/Desktop/xpress is the format to follow
if [ ! -d "/Users/$USER/Desktop/xpress" ]; then
	echo DIR xpress NOT FOUND
	mkdir /Users/$USER/Desktop/xpress
fi

if [ ! -d "/Users/$USER/Desktop/pics" ]; then
	echo DIR pics NOT FOUND
	mkdir /Users/$USER/Desktop/pics
fi

if [ ! -d "/Users/$USER/Desktop/users" ]; then
	echo DIR users NOT FOUND
	mkdir /Users/$USER/Desktop/users
fi

if [ ! -d "/Users/$USER/Desktop/shared" ]; then
	echo DIR shared NOT FOUND
	mkdir /Users/$USER/Desktop/shared
fi

mount -t smbfs //ncdshare/sys2/xpress /Users/$USER/Desktop/xpress
mount -t smbfs //ncdshare/sys2/pics /Users/$USER/Desktop/pics
mount -t smbfs //fileserve2/sys1/users /Users/$USER/Desktop/users
mount -t smbfs //fileserve2/sys1/shared /Users/$USER/Desktop/shared

