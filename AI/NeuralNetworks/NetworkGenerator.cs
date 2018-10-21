﻿using AI.NeuralNetworks.ActivationFunctions;
using AI.RandomNumberGenerators;
using AI.RandomNumberGenerators.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Class that generates different types of networks
    /// </summary>
    public class NetworkGenerator
    {
        private static IRandom random = new StandardBiRandom();

        /// <summary>
        /// Generates a neural net where the layers are fully connected and there are no recurrent connections.
        /// </summary>
        /// <param name="activationFunction"> The activation function that will be assigned to all neurons </param>
        /// <param name="inputLayerSize"> The size of the input layer </param>
        /// <param name="outputLayerSize"> The size of the output layer </param>
        /// <param name="hiddenLayerSizes"> The sizes of the hidden layers </param>
        public static INetwork GenerateFullyConnectedFeedForwardNetwork(IActivationFunction activationFunction, 
            in uint inputLayerSize, in uint outputLayerSize, params uint[] hiddenLayerSizes)
        {
            var rVal = GenerateLayers(activationFunction, inputLayerSize, outputLayerSize, hiddenLayerSizes);

            //connect:
            //input layer to first hidden layer
            FullyConnectLayers(rVal.InputLayer, rVal.HiddenLayers[0]);

            //hidden layer to next hidden layer
            for(int i = 1; i < rVal.HiddenLayers.Length; i++)
            {
                FullyConnectLayers(rVal.HiddenLayers[i - 1], rVal.HiddenLayers[i]);
            }

            //last hidden layer to output layer
            FullyConnectLayers(rVal.HiddenLayers[hiddenLayerSizes.Length - 1], rVal.OutputLayer);


            return rVal;
        }

        /// <summary>
        /// Generates a neural network where the layers are fully connected and there are no recurrent connections
        /// </summary>
        /// <param name="activationFunction"> The activation function that will be assigned to all neurons </param>
        /// <param name="inputLayerSize"> The size of the input layer </param>
        /// <param name="outputLayerSize"> The size of the output layer </param>
        public static INetwork GenerateFullyConnectedFeedForwardNetwork(IActivationFunction activationFunction,
            in uint inputLayerSize, in uint outputlayerSize)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fully connects two layers
        /// </summary>
        /// <param name="layerA">The previous layer</param>
        /// <param name="layerB">The following layer</param>
        private static void FullyConnectLayers(ILayer layerA, ILayer layerB)
        {
            //Generate outgoing connections from layer A
            for(int i = 0; i < layerA.Size; i++)
            {
                var neuron = layerA[i];
                neuron.OutgoingSynapses = layerB.Neurons.Select(x => new Synapse(neuron, x, random.Generate())).ToArray();
            }

            for(int i = 0; i < layerB.Size; i++)
            {
                //As the synapses already exist as ougoing synapses from the layer A we need to select them
                layerB[i].IncomingSynapses = layerA.Neurons.Select(x => x.OutgoingSynapses[i]).ToArray();
            }
        }


        /// <summary>
        /// Helper method that simply generates the neurons of a neural network
        /// </summary>
        /// <param name="random"> An instance of random </param>
        /// <param name="activationFunction"> The activation function that will be assigned to all neurons </param>
        /// <param name="inputLayerSize"> The size of the input layer </param>
        /// <param name="outputLayerSize"> The size of the output layer </param>
        /// <param name="hiddenLayerSizes"> The sizes of the hidden layers </param>
        /// <returns> A network with non-connected neurons </returns>
        private static INetwork GenerateLayers(IActivationFunction activationFunction, 
            in uint inputLayerSize, in uint outputLayerSize, params uint[] hiddenLayerSizes)
        {
            return new Network(
                GenerateLayer(activationFunction, inputLayerSize), 
                hiddenLayerSizes.Select(x => GenerateLayer(activationFunction, x)).ToArray(), 
                GenerateLayer(activationFunction, outputLayerSize)
            );
        }

        /// <summary>
        /// Generates a layer of neurons without setting any connections
        /// </summary>
        /// <param name="random"> An instance of random </param>
        /// <param name="activationFunction"> The activation function that will be assigned to all neurons </param>
        /// <param name="size"> The size of the layer </param>
        /// <returns></returns>
        private static ILayer GenerateLayer(IActivationFunction activationFunction, in uint size)
        {
            var neurons = new INeuron[size];

            for(int i = 0; i < size; i++)
            {
                neurons[i] = new Neuron(random.Generate(), activationFunction);
            }

            return new Layer(neurons);
        }

    }
}