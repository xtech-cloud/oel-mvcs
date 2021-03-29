.PHONY: dll
dll:
	if not exist build\\release mkdir build\\release
	cd build/release & qmake ../../ogm-app.pro -spec win32-g++
	cd build/release & mingw32-make -f Makefile qmake_all
	cd build/release & mingw32-make -j8

