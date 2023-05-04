#-----------------------------------------------------------------------------
# Name:        Recursive Algorithim
# Purpose:     A simple example of a recursive algorithm, using python.
#
# Author:      Tony Lin
# Created:     04-Apr-2023
# Updated:     11-Apr-2023
#-----------------------------------------------------------------------------

memoization = {}

def fractionSimplifier(numerator : int, denominator : int, factor=1, gcf=1) -> tuple:
    '''
    Simplifies improper fractions in O(n) time.

    Parameters 
    ----------
    numerator : int
        The numerator of the fraction.
    denominator : int
        The denomintaor of the fraction.
    factor : int
        The current factor of the algorithm.
    gcf : int
        The greatest common factor found so far.
    
    Returns
    ----------
    tuple
        A tuple containing the simplified fraction (numerator, denominator).
    '''

    # check if it is inside the memoization table first
    if (numerator, denominator) in memoization:
        return memoization[(numerator, denominator)]
    
    # not inside the table, check if they are divisible by current iteration
    if numerator % factor == 0 and denominator % factor == 0:
        gcf = factor
    
    # reached the end of possible values, return gcf and add to memoization
    if factor == numerator or factor == denominator:
        memoization[(numerator, denominator)] = (numerator//gcf, denominator//gcf)
        return (numerator//gcf, denominator//gcf)
    # otherwise, call itself again
    return fractionSimplifier(numerator, denominator, factor+1, gcf)

while True:
    # ask user for fraction
    numerator = int(input("Enter the numerator: "))
    denominator = int(input("Enter the denominator: "))
    # input validation
    if numerator == 0 or denominator == 0: 
        print("0 is not a valid input")
    else:
        # the answer
        try:
            answer = fractionSimplifier(numerator, denominator)
            print(f"{answer[0]} / {answer[1]}")
        # catch recursion depth error
        except RecursionError as err:
            print("This program was unable to complete due to recursion depth, please try with a smaller fraction.")
    

