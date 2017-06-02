# Pipelining

[![][build-img]][build]
[![][nuget-img]][nuget]

[build]:     https://ci.appveyor.com/project/TallesL/net-Pipelining
[build-img]: https://ci.appveyor.com/api/projects/status/github/tallesl/net-Pipelining
[nuget]:     https://www.nuget.org/packages/Pipelining
[nuget-img]: https://badge.fury.io/nu/Pipelining.svg

Write programs in a pipeline fashion.

## Example

```cs
// A class inheriting from PipelineGroup.
// A group manages both the building and running of pipelines.
// It also acts as the "scope" of the registered pipelines (determining which pipelines can reference each other).
public class GuessTheNumberPipelines : PipelineGroup
{
    // We just define a constructor where the pipeline registering will take place.
    public GuessTheNumberPipelines()
    {
        // Registering a new pipeline with an ID of "guess_the_number".
        Pipeline("guess_the_number")

            // Instantiating a pipe and adding it to the pipeline.
            .Pipe(new RandomNumberPipe(1 , 10))

            // Creating a new pipeline with an ID of "divisible" and also adding it to this pipeline.
            // This pipeline is registered to this group as any other pipeline, registering inside another pipeline
            // is just a convinience of doing two steps in one (both creating a new pipeline and referecing it).
            .Pipeline("divisible", p => p
                .Pipe(new DivisibleBy(2))
                .Pipe(new DivisibleBy(3))
                .Pipe(new DivisibleBy(4))
                .Pipe(new DivisibleBy(5)))

            // Same thing again, registering and referencing a pipeline with an ID of "ask".
            .Pipeline("ask", p => p

                // Adding a IPipe to the pipeline, this time by its type.
                // Since it's the library that will instantiate this one, is mandatory that the IPipe implementation
                // has a default constructor.
                .Pipe<AskAnswerPipe>()

                // Adding a special kind of pipe, an IBranchPipe.
                // This one allows you to branch into other pipelines (in the same group).
                .BranchPipe<CheckAnswerPipe>()

                    // Specifying that it can branch to a pipeline called "success" (and creating it at the same
                    // time).
                    .BranchTo("success", _p => _p
                        .Pipe<SuccessPipe>()
                        .Pipeline("try_again", __p => __p
                            // Another branch, this time in the inner (of the inner) pipeline.
                            .BranchPipe<TryAgainPipe>()
                            .BranchTo("guess__number"))) // Yes, you can loop!

                    // The rest below is similar to what has been shown.
                    .BranchTo("failure", _p => _p
                        .Pipe<FailurePipe>()
                        .Pipe("try_again"))

                    .BranchTo("invalid", _p => _p
                        .Pipe<InvalidAnswerPipe>()
                        .Pipe("ask")));
    }
}
```

You can check the implementation of any individual pipe [here] or [download the project] and run the [test application]
if you prefer.

[here]: TestApplication/Pipes
[download the project]: https://github.com/tallesl/net-Pipelining/archive/master.zip
[test application]: TestApplication

## Concepts

* `PipelineGroup`: A group of pipelines that can reference each other.
* `IPipe`: Pipe that can be chained into a pipeline.
* `IBranchPipe`: Pipe that can be branched into a different pipeline.
* `PipelineEnd`: Ends a pipeline execution prematurely.
* `PipelineResult`: Result from a pipeline run.
* `PipeResult`: Result from an IPipe run.
* `FriendlyPipelineResult`:A  serialize-friendly version of PipelineResult.
* `FriendlyPipeResult`: A serialize-friendly version of PipeResult.