#!  /bin/python3

import networkx as nx
from matplotlib import pyplot as plt


G = nx.Graph()
nodes = ['WA', 'NT', 'Q', 'NSW', 'V', 'SA', 'T']
G.add_nodes_from(nodes)
edges = [('WA', 'NT'), ('NSW', 'V'), ('WA', 'SA')]
G.add_edges_from(edges)
plt.figure(figsize=(12,6))
plt.show()
