#include <iostream>
#include <stdlib.h>
#include <time.h>
#include <cstdio>

using namespace std;

int main()
{

    int rozmiar;
    cout <<"Podaj rozmiar tablicy A : ";
    cin>>rozmiar;
    srand(time(NULL));
    int A[rozmiar];
    int maxA=0;
    int size_tab = sizeof(A)/sizeof(A[0]);
    for(int i=0;i<=size_tab;i++)
        A[i]=(rand() % 100 )+1;

    cout<<"A: ";
    for(int i=0;i<=size_tab;i++)
        cout<<A[i] <<" ";

    cout <<"\n";

    cout <<"Rozmiar tablicy A : " <<size_tab;
    for(int i=0;i<=size_tab-1;i++)
    {
        if(A[i]>maxA)
            maxA=A[i];
    }
    cout <<"\nMaxymalny tab A : " <<maxA <<"\n";
    int C[maxA];
    clock_t start = clock();
    for(int i=0;i<=maxA;i++)
        C[i]=0;

    for(int i=0;i<size_tab;i++)
        C[A[i]]=C[A[i]]+1;

    for(int i=1;i<=maxA;i++)
        C[i]=C[i]+C[i-1];

    int B[size_tab];

    for(int i=size_tab-1;i>=0;i--)
    {
        B[C[A[i]]-1]=A[i];
        C[A[i]]=C[A[i]]-1;
    }

    for(int i=0;i<=size_tab-1;i++)
        cout<<B[i] <<" ";
    cout <<"\nCzas sortowania : " <<clock()-start <<"ms\n";
}
