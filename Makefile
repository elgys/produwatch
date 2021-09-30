GCC = mcs

PROG = stopwatch.exe


compile:
	$(GCC) -out:$(PROG) *.cs

run:
	mono $(PROG) 
