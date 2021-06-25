string swapLexOrder(string str, int[][] pairs) {
    // If 1 can swap with 3, and 3 can swap with 4, then 1 can eventually swap with 4. Transitive.
    // For this reason we can build a graph -- connected components that have all the positions of possible
    // would-be swaps. 
    // We end up just putting the sorted letters (vertices) in the positions that would be possible to swap them to.

    // 1. Build adjacency list (beginner/intermediate graph theory)
    var adjacencyLists = new Dictionary<int, List<int>>();
    foreach(var pair in pairs)
    {
        var A_vertex = pair[0];
        var B_vertex = pair[1];

        if(!adjacencyLists.ContainsKey(A_vertex))
            adjacencyLists.Add(A_vertex, new List<int>());

        if(!adjacencyLists.ContainsKey(B_vertex))
            adjacencyLists.Add(B_vertex, new List<int>());

        adjacencyLists[A_vertex].Add(B_vertex);
        adjacencyLists[B_vertex].Add(A_vertex);
    }

    // 2. Create connected components (beginner/intermediate graph theory). I'm using breadth-first search for no reason in particular. This solution could use BFS or DFS -- queue or stack respectively. Just flip a coin.
    var visited = new HashSet<int>();
    var connectedComponents = new HashSet<List<int>>();
    foreach(var vertex in adjacencyLists.Keys)
    {
        if(visited.Contains(vertex)) continue;

        var currentConnectedComponent = new List<int>();
        connectedComponents.Add(currentConnectedComponent);
        var vertexQueue = new Queue<int>();
        vertexQueue.Enqueue(vertex);

        while(vertexQueue.Count != 0)
        {
            var currentVertex = vertexQueue.Dequeue();
            
            if(visited.Contains(currentVertex)) continue;

            foreach(var adjacentVertex in adjacencyLists[currentVertex])
                vertexQueue.Enqueue(adjacentVertex);

            currentConnectedComponent.Add(currentVertex);
            visited.Add(currentVertex);
        }
    }

    // 3. Get characters in connected component (connected component = valid swap positions -- connected by swaps) then sort letters in reverse order, then place back into those positions. 
    var newString = str.ToCharArray();
    var vertexCharacters = new List<char>();
    foreach(var connectedComponent in connectedComponents)
    {
        connectedComponent.Sort();
        foreach(var index in connectedComponent)
            vertexCharacters.Add(newString[index - 1]);

        vertexCharacters.Sort();
        vertexCharacters.Reverse();

        foreach(var index in connectedComponent)
        {
            newString[index - 1] = vertexCharacters[0];
            vertexCharacters.RemoveAt(0);
        }
    }   

    return new string(newString);
}