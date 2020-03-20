# -*- coding: utf-8 -*-
import numpy as np
import cv2

def convert(img_path):

    ## 训练knn模型
    samples = np.load('samples.npy')
    labels = np.load('label.npy')
    path =img_path
    k = 80
    train_label = labels[:k]
    train_input = samples[:k]
    test_input = samples[k:]
    test_label = labels[k:]

    model = cv2.ml.KNearest_create()
    model.train(train_input,cv2.ml.ROW_SAMPLE,train_label)
    img = cv2.imread(path)
    gray = cv2.cvtColor(img,cv2.COLOR_BGR2GRAY)
    ## 阈值分割
    ret,thresh = cv2.threshold(gray,200,255,1)

    kernel = cv2.getStructuringElement(cv2.MORPH_CROSS,(5, 5))     
    dilated = cv2.dilate(thresh,kernel)
 
    ## 轮廓提取
    contours, hierarchy = cv2.findContours(dilated,cv2.RETR_TREE,cv2.CHAIN_APPROX_SIMPLE)

    ##　提取八十一个小方格
    boxes = []
    for i in range(len(hierarchy[0])):
        if hierarchy[0][i][3] == 0:
            boxes.append(hierarchy[0][i])
        
    height,width = img.shape[:2]
    box_h = height/9
    box_w = width/9
    number_boxes = []
    ## 数独初始化为零阵
    soduko = np.zeros((9, 9),np.int32)

    for j in range(len(boxes)):
        if boxes[j][2] != -1:
            #number_boxes.append(boxes[j])
            x,y,w,h = cv2.boundingRect(contours[boxes[j][2]])
            number_boxes.append([x,y,w,h])
            #img = cv2.rectangle(img,(x-1,y-1),(x+w+1,y+h+1),(0,0,255),2)
            #img = cv2.drawContours(img, contours, boxes[j][2], (0,255,0), 1)
            ## 对提取的数字进行处理
            number_roi = gray[y:y+h, x:x+w]
            ## 统一大小
            resized_roi=cv2.resize(number_roi,(20,40))
            thresh1 = cv2.adaptiveThreshold(resized_roi,255,1,1,11,2) 
            ## 归一化像素值
            normalized_roi = thresh1/255.  
        
            ## 展开成一行让knn识别
            sample1 = normalized_roi.reshape((1,800))
            sample1 = np.array(sample1,np.float32)
        
            ## knn识别
            retval, results, neigh_resp, dists = model.findNearest(sample1, 1)        
            number = int(results.ravel()[0])
        
            ## 识别结果展示
            cv2.putText(img,str(number),(x+w+1,y+h-20), 3, 2., (255, 0, 0), 2, cv2.LINE_AA)
        
            ## 求在矩阵中的位置
            soduko[int(y/box_h)][int(x/box_w)] = number
    return soduko     
            #print(number)
           # cv2.namedWindow("img", cv2.WINDOW_NORMAL); 
           # cv2.imshow("img", img)
           # cv2.waitKey(30)
