# /etc/init.d/cardReader.py
### BEGIN INIT INFO
# Provides:          cardReader.py
# Required-Start:    $remote_fs $syslog
# Required-Stop:     $remote_fs $syslog
# Default-Start:     2 3 4 5
# Default-Stop:      0 1 6
# Short-Description: Start daemon at boot time
# Description:       Enable service provided by daemon.
### END INIT INFO

#! /usr/bin/env python

import time
from sys import stdin
from socket import *
from datetime import datetime
import RPi.GPIO as GPIO

buzzer_pin = 27
LokaleName = "J104"

notes = {
	'B0' : 31,
	'C1' : 33, 'CS1' : 35,
	'D1' : 37, 'DS1' : 39,
	'EB1' : 39,
	'E1' : 41,
	'F1' : 44, 'FS1' : 46,
	'G1' : 49, 'GS1' : 52,
	'A1' : 55, 'AS1' : 58,
	'BB1' : 58,
	'B1' : 62,
	'C2' : 65, 'CS2' : 69,
	'D2' : 73, 'DS2' : 78,
	'EB2' : 78,
	'E2' : 82,
	'F2' : 87, 'FS2' : 93,
	'G2' : 98, 'GS2' : 104,
	'A2' : 110, 'AS2' : 117,
	'BB2' : 123,
	'B2' : 123,
	'C3' : 131, 'CS3' : 139,
	'D3' : 147, 'DS3' : 156,
	'EB3' : 156,
	'E3' : 165,
	'F3' : 175, 'FS3' : 185,
	'G3' : 196, 'GS3' : 208,
	'A3' : 220, 'AS3' : 233,
	'BB3' : 233,
	'B3' : 247,
	'C4' : 262, 'CS4' : 277,
	'D4' : 294, 'DS4' : 311,
	'EB4' : 311,
	'E4' : 330,
	'F4' : 349, 'FS4' : 370,
	'G4' : 392, 'GS4' : 415,
	'A4' : 440, 'AS4' : 466,
	'BB4' : 466,
	'B4' : 494,
	'C5' : 523, 'CS5' : 554,
	'D5' : 587, 'DS5' : 622,
	'EB5' : 622,
	'E5' : 659,
	'F5' : 698, 'FS5' : 740,
	'G5' : 784, 'GS5' : 831,
	'A5' : 880, 'AS5' : 932,
	'BB5' : 932,
	'B5' : 988,
	'C6' : 1047, 'CS6' : 1109,
	'D6' : 1175, 'DS6' : 1245,
	'EB6' : 1245,
	'E6' : 1319,
	'F6' : 1397, 'FS6' : 1480,
	'G6' : 1568, 'GS6' : 1661,
	'A6' : 1760, 'AS6' : 1865,
	'BB6' : 1865,
	'B6' : 1976,
	'C7' : 2093, 'CS7' : 2217,
	'D7' : 2349, 'DS7' : 2489,
	'EB7' : 2489,
	'E7' : 2637,
	'F7' : 2794, 'FS7' : 2960,
	'G7' : 3136, 'GS7' : 3322,
	'A7' : 3520, 'AS7' : 3729,
	'BB7' : 3729,
	'B7' : 3951,
	'C8' : 4186, 'CS8' : 4435,
	'D8' : 4699, 'DS8' : 4978
}

CHECKIN_melody = [
	notes['B0'],notes['B0'],
]

CHICKIN_tempo = [
	1,1,
]

CHECKOUT_melody = [
	notes['E5'],
]

CHICKOUT_tempo = [
	1,
]

ERROR_melody = [
	notes['D4'], notes['D4'], notes['D4'],
]

ERROR_tempo = [
	1,1,1,
]


def buzz(frequency, length):	 #create the function "buzz" and feed it the pitch and duration)

	if(frequency==0):
		time.sleep(length)
		return
	period = 1.0 / frequency 		 #in physics, the period (sec/cyc) is the inverse of the frequency (cyc/sec)
	delayValue = period / 2		 #calcuate the time for half of the wave
	numCycles = int(length * frequency)	 #the number of waves to produce is the duration times the frequency
	
	for i in range(numCycles):		#start a loop from 0 to the variable "cycles" calculated above
		GPIO.output(buzzer_pin, True)	 #set pin 27 to high
		time.sleep(delayValue)		#wait with pin 27 high
		GPIO.output(buzzer_pin, False)		#set pin 27 to low
		time.sleep(delayValue)		#wait with pin 27 low

def play(melody,tempo,pause,pace=0.800):
	
	for i in range(0, len(melody)):		# Play song
		
		noteDuration = pace/tempo[i]
		buzz(melody[i],noteDuration)	# Change the frequency along the song note
		
		pauseBetweenNotes = noteDuration * pause
		time.sleep(pauseBetweenNotes)
		
def setup():
	GPIO.setmode(GPIO.BCM)
	GPIO.setup(buzzer_pin, GPIO.IN)
	GPIO.setup(buzzer_pin, GPIO.OUT)
	




IP = ""
PORT = 8547

INPORT = 9876

s = socket(AF_INET, SOCK_DGRAM)
s.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)
s.bind((IP, INPORT))



def sendData(uinput):
	setup()
	data = "CARD/" + LokaleName + "/"  + str(datetime.now()) + "/" + uinput
	s.sendto(bytes(data, "UTF-8"), ('<broadcast>', PORT))
		
	data, addr = s.recvfrom(4096)
	print(data)

	decodedData = data.split(b'/')

	if decodedData[1] == b"CHECKIN":
		play(CHECKIN_melody, CHICKIN_tempo, 0.10, 0.200)
		print("IN")

	if decodedData[1] == b"CHECKOUT":
		play(CHECKOUT_melody, CHICKOUT_tempo, 0.10, 0.400)
		print("OUT")
	
	if decodedData[1] == b"ERROR":
		
		play(ERROR_melody, ERROR_tempo, 0.10, 0.200)
		print("ERROR")

	GPIO.cleanup()
	
def starter():
	setup()	
	play(CHECKIN_melody, CHICKIN_tempo, 0.10, 0.200)
	play(CHECKOUT_melody, CHICKOUT_tempo, 0.10, 0.400)
	play(ERROR_melody, ERROR_tempo, 0.10, 0.200)
	print("Ready and booted...")
	GPIO.cleanup()
	
starter()
while True:
	userinput = stdin.readline()
	sendData(userinput)
