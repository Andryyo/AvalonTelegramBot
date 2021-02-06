using Avalon.Core.Enums;
using Avalon.Core.Interfaces;
using Avalon.Core.Phases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalon.Core.Models
{
    public class TeamBuildingPhase : IPhase
    {
        public TeamBuildingPhase(
            IAvalonContext context,
            int round,
            int unsuccessfullVotes)
        {
            Context = context;
            Round = round;
            UnsuccessfullVotes = unsuccessfullVotes;
        }

        int Round { get; set; }
        public int UnsuccessfullVotes { get; }

        private const int MaxUnsessessfullVotesInARow = 5;
        Dictionary<int, Dictionary<int, int>> teamSizesTable = new Dictionary<int, Dictionary<int, int>>()
        {
            { 5, new Dictionary<int, int>() { { 1, 2 }, { 2, 3}, { 3, 2}, { 4, 3}, { 5, 3} } },
            { 6, new Dictionary<int, int>() { { 1, 2 }, { 2, 3}, { 3, 4}, { 4, 3}, { 5, 4} } },
            { 7, new Dictionary<int, int>() { { 1, 2 }, { 2, 3}, { 3, 3}, { 4, 4}, { 5, 4} } },
            { 8, new Dictionary<int, int>() { { 1, 3 }, { 2, 4}, { 3, 4}, { 4, 5}, { 5, 5} } },
            { 9, new Dictionary<int, int>() { { 1, 3 }, { 2, 4}, { 3, 4}, { 4, 5}, { 5, 5} } },
            { 10, new Dictionary<int, int>() { { 1, 3 }, { 2, 4}, { 3, 4}, { 4, 5}, { 5, 5} } },

        };

        public IAvalonContext Context { get; }

        public async Task<IPhase> Execute()
        {
            var teamSize = GetTeamSize(Context.Users.Count(), Round);
            await Context.Leader.SendMessage(string.Format("{0}, please assign {0} players to quest team", Context.Leader.Name, teamSize));
            var team = await Context.Leader.SelectUsers(Context.Users, teamSize);

            await Context.SendMessage(string.Format("Proposed team is {0}", string.Join(", ", team.Select(user => user.Name))));
            TransferLeadership();

            var votes = await Task.WhenAll(Context.Users.Select(x => Task.Run(async () => new { User = x, Vote = await x.SelectVoteToken() })));

            await Context.SendMessage(string.Format("Votes are:\r\n{0}", string.Join("\r\n", votes.Select(v => string.Format("{0} - {1}", v.User.Name, v.Vote)))));

            if (votes.Count(x => x.Vote == VoteToken.VoteApproved) > votes.Count(x => x.Vote == VoteToken.VoteRejected))
            {
                await Context.SendMessage("Team is approved");

                return new QuestPhase(Context, team, Round);
            }
            else
            {
                if (UnsuccessfullVotes == MaxUnsessessfullVotesInARow)
                {
                    await Context.SendMessage("Uncessessfull votes limit reached");

                    return new EvilWonPhase(Context);
                }
                else
                {
                    await Context.SendMessage(string.Format("Team not approved, attempts {0}", UnsuccessfullVotes + 1));

                    return new TeamBuildingPhase(Context, Round, UnsuccessfullVotes + 1);
                }
            }
        }

        private void TransferLeadership()
        {
            var leaderIndex = Context.Users.IndexOf(Context.Leader);
            Context.Leader = Context.Users.ElementAt(leaderIndex == Context.Users.Count - 1 ? 0 : leaderIndex + 1);
        }

        public int GetTeamSize(int playersCount, int round)
        {
            return teamSizesTable[playersCount][round];
        }
    }
}
