import sys

def Hello(name):
	print("hello print")
	print("Hello" + name)
	return name

if __name__ == "__main__":
	args = sys.argv[1:]

	result = Hello(args[0])
	print(result)