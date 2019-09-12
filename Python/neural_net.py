# -*- coding: utf-8 -*-
"""
Created on Thu Jun 27 10:36:29 2019

@author: DanielHampikian
"""

import numpy as np

class NeuralNetwork:
    def __init__(self,x,y):
        np.random.seed(1)
        self.input = x
        self.weights1 = 2 * np.random.rand(self.input.shape[1],4) - 1
        self.weights2 = 2 * np.random.rand(4,1) - 1
        self.y = y
        self.output = np.zeros(y.shape)
        self.layer1 = np.dot(x,self.weights1)
        self.layer2 = np.dot(self.layer1,self.weights2)
    def toString(self):
        return("input:\n" + str(self.input) + "\n" + "training output:\n" + str(self.y) + "\n" + "w1:\n" + str(self.weights1) + "\n" + "w2:\n" + str(self.weights2) + "\n" + "actual output:\n" + str(self.layer2))
#sigmoid function maps to a value between 0 and 1
    def sigmoid_derivative(self, x):
        return (self.sigmoid(x) * (1-self.sigmoid(x)))
    def sigmoid(self, x):
        return 1/(1+np.exp(-x))
    def feedforward(self):
        self.layer1 = self.sigmoid(np.dot(self.input,self.weights1))
        self.layer2 = self.sigmoid(np.dot(self.layer1,self.weights2))
        
    def backprop(self):
        # application of the chain rule to find derivative of the loss function with respect to weights2 and weights1
        l2_error = self.y - self.layer2
        l2_delta = l2_error * self.sigmoid_derivative(self.layer2)
        l1_error = l2_delta.dot(self.weights2.T)
        l1_delta = l1_error * self.sigmoid_derivative(self.layer1)
        d_weights2 = np.dot(self.layer1.T, l2_delta)
        d_weights1 = np.dot(self.input.T, l1_delta)

        # update the weights with the derivative (slope) of the loss function
        self.weights1 += d_weights1
        self.weights2 += d_weights2
    def train(self, num):
        for x in range(num):
            self.feedforward()
            self.backprop()
    def save(self, filename):
        with open(filename, "w") as fout:
            fout.write(str(self.weights1)+"\n"+str(self.weights2))
    def load(self,filename):
        w_array = []
        with open(filename,"r") as fin:
            for line in fin:
                w_array.append(np.array(line.strip().split()))
        self.weights2 = w_array.pop()
        self.weights1 = w_array.pop()
    def predict(self,input_array,filename=None):
        if filename!=None:
            self.load(filename)
        self.input = input_array
        self.feedforward()
        return self.toString()

#initialize the input dataset as a matrix (an array of vectors):
x = np.array([[0,0,1],[0,1,1],[1,0,1],[1,1,1]])
#initialize the training output dataset as matrix
y = np.array([[0],[1],[1],[0]])
nn = NeuralNetwork(x,y)
print(nn.toString())
nn.train(100000)
#print(nn.toString())
print(nn.predict([[0,0,1],[0,1,1],[1,0,1],[1,1,1]]))
