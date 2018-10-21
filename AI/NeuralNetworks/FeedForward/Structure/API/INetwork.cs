﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks.FeedForward
{
    /// <summary>
    /// Interface representing a neural net
    /// </summary>
    public interface INetwork
    {
        /// <summary>
        /// Get all the hidden layers of the net
        /// </summary>
        ILayer[] HiddenLayers { get; }

        /// <summary>
        /// The input layer of this net
        /// </summary>
        ILayer InputLayer { get; }

        /// <summary>
        /// The output layer of this net
        /// </summary>
        ILayer OutputLayer { get; }


        void Calculate(double[] inputValues);
    }
}