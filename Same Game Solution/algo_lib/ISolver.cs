using System.Collections.Generic;

namespace Same_Game_Solution.algo_lib
{
    // public interface ICountdownSolver<in TProblem, out TSolution> : ISolver<TProblem,TSolution>

    public interface ISolution
    {
        double Score { get; }
    }
    public interface ISolver<in TProblem, out TSolution>
        where TSolution : ISolution
    { 
        /// <param name="problem">problem to solve</param>
        /// <param name="countdown">max time to use</param>
        /// <returns>
        ///     sequence of solution improvements. The last one is the best one.
        ///     You may return only one the best solution, but sequence of improvements can help in debugging
        /// </returns>
        IEnumerable<TSolution> GetSolutions(TProblem problem, Countdown countdown);
    }
}