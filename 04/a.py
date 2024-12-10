import numpy as np
from skimage.util.shape import view_as_windows as viewW

def main():
    # Read numeric values of chars to np array
    lr = np.genfromtxt("input", dtype='str', delimiter=1).view(np.int32)
    rl = np.fliplr(lr)
    ud = np.rot90(lr)
    du = np.rot90(rl)
    r1 = diagonalize(lr)
    r2 = diagonalize(rl)
    r3 = diagonalize(ud)
    r4 = diagonalize(du)

    sum = 0
    for i, ws in enumerate([lr, rl, ud, du, r1, r2, r3, r4]):
        words = ws.view('U1')
        asd = np.sum(np.apply_along_axis(lambda r: ''.join(r).count("XMAS"), 1, words))
        sum += asd

    print(sum)

def diagonalize(arr):
    r, _ = arr.shape
    padded = np.pad(arr, ((0,r - 1),(0,0)), mode='constant')
    pf = np.rot90(padded, 1)
    pff = strided_indexing_roll(pf, np.flip(np.arange(r)))
    pfff = np.rot90(pff, 3)
    return pfff

# https://stackoverflow.com/a/51613442
def strided_indexing_roll(a, r):
    # Concatenate with sliced to cover all rolls
    a_ext = np.concatenate((a,a[:,:-1]),axis=1)

    # Get sliding windows; use advanced-indexing to select appropriate ones
    n = a.shape[1]
    return viewW(a_ext,(1,n))[np.arange(len(r)), (n-r)%n,0]

if __name__ == "__main__":
    main()
