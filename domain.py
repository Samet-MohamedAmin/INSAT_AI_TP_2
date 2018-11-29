
from math import sqrt


class Domain:
    def __init__(self, n):
        self.LINES = int(sqrt(n))
        self.values = range(n)
        self.used = []

    @staticmethod
    def get_pos(self, pos):
        return int(pos / self.LINES) + 1,\
               int(pos % self.LINES) + 1