#!/bin/bash
# Script to read Amazon reports within a directory, grab the values for 
# Processed files and Successful files, calculate the difference and then
# output the results to a text file.
# JH 4-5-17

counter=0
for filename in processingreports/*.txt; do
	counter=$((counter+1))
	echo $filename
	DATE=$(stat -c %y "$filename" | cut -c1-19) 
	#echo $DATE
	PROCESSED=$(sed -n '/Number of records processed/p' $filename | awk '{print $5}')

	SUCCESS=$(sed -n '/Number of records successful/p' $filename | awk '{print $5}')
	#echo $PROCESSED
	#echo $SUCCESS

	DIFF=`expr $PROCESSED - $SUCCESS`
	#echo $DIFF

	echo $DATE, $PROCESSED, $SUCCESS, $DIFF >> amazonOutput.txt
done
echo Done processing $counter files
