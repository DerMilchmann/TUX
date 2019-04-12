﻿using System.Collections;
using System.Data;
using UnityEngine;

namespace BML_ExperimentToolkit.Scripts.ExperimentParts.SimpleExperimentParts {


    /// <inheritdoc />
    /// <summary>
    /// This is the simplest trial possible. It is used when no custom trial is specified.
    /// This trial overwrites only the MainCoroutine() method, making the program wait for the return key to be pressed.
    /// The trial ends when the key is pressed.
    /// </summary>
    public class SimpleTrial : Trial {

        // ReSharper disable once NotAccessedField.Local
        SimpleExperimentRunner simpleExperimentRunner;

        /// <summary>
        /// Constructor just calls the base class Trail's constructor.
        /// </summary>
        /// <param name="runner">The Runner being run</param>
        /// <param name="data">The row of BlockData for this trial from a table</param>
        public SimpleTrial(ExperimentRunner runner, DataRow data) : base(runner, data) {}

        public override void PreMethod() {
            simpleExperimentRunner = (SimpleExperimentRunner) Runner;
        }

        /// <inheritdoc />
        /// <summary>
        /// Overwrites the MainCoroutine method to provide the trial's functionality
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator RunMainCoroutine() {
            bool running = true;
            Debug.Log("...Waiting for you to press return key! (in SimpleTrial MainCoroutine() method)");
            while (running) {
                if (Input.GetKeyDown(KeyCode.Return)) {
                    //Debug.Log($"{TrialText} Return key pressed!");
                    running = false;
                }

                yield return null;
            }
        }
    }
}