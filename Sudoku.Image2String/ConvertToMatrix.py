import os
import sys
import Image2String as i2s

if __name__ == '__main__':
 print(i2s.convert(sys.argv[1])) #表示传递进来的参数，sys.argv[0]表示文件本身名字，从1开始，依次类推
