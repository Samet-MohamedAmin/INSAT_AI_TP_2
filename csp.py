#! /bin/python3
from queen import Queen
from domain import Domain

class csp:
    def __init__(self, domain, constraints, vars):
        self.domain = domain
        self.constraints = constraints
        self.vars = vars

        # n_reines, sudoku

    def add_constraints(self):
        pass

    def is_valid(self, index):
        pass

    def backtracking(self):
        for v in self.vars:
            for index in range(len(self.domain)):
                if self.is_valid(index):
                    self.domain[index]['value'] = v
                    result = self.backtracking()
                    if result: return result
                    self.domain[index]['value'] = 0
                else:
                    return False


if __name__ == '__main__':
    constraints = [
        ''
        '',
        'q.col != q2.col'
    ]

    n_reines = csp(Domain(16), constraints, [Queen(x) for x in range(4)])


