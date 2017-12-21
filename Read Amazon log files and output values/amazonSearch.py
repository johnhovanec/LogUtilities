import re
import os
import itertools
from datetime import datetime, timezone

# Script to read Amazon upload files, parse out values for lines containing the strings 'processed' and 'successful', and
# then output the values, the difference, along with a timestamp to a text file.
# Note: in the rare case where a file uses non-standard wording, I'm only searching for the minimal identifiable strings,
# which are 'processed' and 'successful'
#
# To Run: Need Python 3 installed, navigate to the Python directory or add Path to Environment Variables, 
# then enter: python amazonSearch.py
#
# John Hovanec 4-5-17

# folder location
rootdir = 'C:\cygwin\home\jhovanec\processingreports'

# counter to track files processed
counter=0
for subdir, dirs, files in os.walk(rootdir):
	for file in files:
		counter += 1

		print (file)
		with open(rootdir + '\/' + file) as f:
			for i, line in enumerate(f, 1):
				if i == 2:
					proc = line.split("processed")[-1].split()[0] 	
					#print (proc)
				elif i == 3:
					succ = line.split("successful")[-1].split()[0]
					#print (succ)

			# get the difference
			diff = int(proc) - int(succ)

			# get file modification time
			date = os.path.getmtime(rootdir + '\/' + file)
			utc_time = datetime.fromtimestamp(date, timezone.utc)
			readable_datetime = utc_time.astimezone().strftime("%Y-%m-%d %H:%M:%S")

			# write to file
			with open("AmazonOutput_py.txt", "a") as out:
						out.write(readable_datetime + ', ' + proc + ', ' + succ + ', ' + str(diff) + '\n')		

print ("\nDone processing " + str(counter) + " files.")				
				
			