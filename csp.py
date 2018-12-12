#! /bin/python3

import main as main_program

class csp:
    def __init__(self, domain, vars, result, problem):
        self.domain = domain
        # self.constraints = constraints
        self.vars = vars
        self.result = result
        self.problem = problem

        # n_reines, sudoku

    def place_or_not_place(self, line, variable):
        if self.problem == 1:
            if line == 0: return True
            for i in range(line):
                if (abs(int(variable) - int(self.result[i])) == abs(line - i)) and int(self.result[i]) != 0 \
                        or int(variable) == int(self.result[i]):
                    return False
            return True

        else:
            if line == 0: return True
            for i in range(5):
                if (self.result[i] == self.result[5]) and not (self.result[i] == 0):
                    return False
            for i in range(4):
                if (self.result[i] == self.result[i+1]) and not (self.result[i] == 0):
                    return False

            return True

    def backtrack_solve(self, line):
        if line >= len(self.vars): return True
        else:
            for d in self.domain:
                if self.place_or_not_place(line, d):
                    self.result[line] = d
                    if self.backtrack_solve(line+1):
                        return True

                    self.result[line] = '0'

        return False

    def print_solution(self, n):
        print('-'*10)
        for i in range(n):
            print('%s is set to %s' %(self.vars[i], self.result[i]))

    def remove(self, tmp, line):
        FD = [x[:] for x in tmp] # Array.Copy(tmp, FD, tmp.Length)
        for i in list(range(line+1, len(tmp))):
            for v in FD[i]:
                result[i] = v
                if not self.place_or_not_place(i, v):
                    if v in FD[i]:
                        del FD[i][v] # FD[i].RemoveAt(index)
                result[i] = '0'

    def forward_solve(self, forward_domain, line,):
        if line >= len(self.vars): return True
        else:
            for i in range(line, len(forward_domain)):
                # if forward_domain[i].count() == 0: return False
                for d in forward_domain[i]:
                    if self.place_or_not_place(line, d):
                        self.result[line] = d
                        original = []
                        for k in range(forward_domain):
                            original[k] = []
                            for item in forward_domain:
                                original[k].append(item)
                        forward_domain = self.remove(forward_domain, line)
                        # if you want to see the trace
                        #
                        if self.forward_solve(forward_domain, line + 1): return True
                        forward_domain = original
                        result[line] = '0'



if __name__ == '__main__':

    print('choose the problem')
    p_choice = 1 # int(input('[1] n queens \t [2] map filling'))
    print('choose the algorithm')
    s_choice = 1 #int(input('[1] backtrack \t [2] forward'))

    if p_choice == 1:
        """n_queens"""
        variables = ['Q'+str(i) for i in range(8)]
        domain = list(range(1, 9))
        result = [0]*8
        p = csp(domain, variables, result, p_choice)
        if s_choice == 1:
            if not p.backtrack_solve(0):
                print('there\'s no solution')
            p.print_solution(8)

            main_program.main(p.final_result)

        elif s_choice == 2:
            forward_domain = []
            for i in range(8):
                forward_domain.append(list(range(1, 9)))
            if not p.forward_solve(forward_domain, 0):
                print('THERE\'S NO SOLUTION')
            p.print_solution(8)

    elif p_choice == 2:
        """map filling"""
        variables = ['WA', 'NT', 'Q', 'NSW', 'V', 'SA', 'T']
        domain = ['R', 'G', 'B']
        result = [0]*7

        p = csp(domain, variables, result, p_choice)

        if s_choice == 1:
            if not p.backtrack_solve(0):
                print('there\'s no solution')
            p.print_solution(7)

        if s_choice == 2:
            forward_domain = []
            for i in range(8):
                forward_domain.append(list(domain))
            if not p.forward_solve(forward_domain, 0):
                print('THERE\'S NO SOLUTION')
            p.print_solution(7)
