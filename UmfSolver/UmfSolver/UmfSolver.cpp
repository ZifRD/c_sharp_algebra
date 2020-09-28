// UmfSolver.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <string.h>
#include <math.h>
#include "umfpack.h"
extern "C" __declspec(dllexport) void UmfpackSolve()
{
    char basstring[10];
	FILE* inbas;
	FILE* out;
	FILE* inright;
	int n;
	int i;
	int Axc =0;
	int Apc =0;
	int Aic = 0;
	int* Ap;
	int* Ai;
	double* Ax;
	double* b;
	double* x;
	double *null = (double *) NULL ;
    void *Symbolic, *Numeric ;
	
	int bas;
	int c;
	int outx;

	char cMassiv[120];
	char *szStr = "";
    char szStr2[20];
	int xGross;
	int xKlein;

	inright = fopen("right.txt","r");
	fscanf(inright,"%d,%s\n",&bas,basstring);
	n =1;
	for (i = 2; i <= bas;i++)
	{
       n *= i; 
	}
	b = new double[n];
	x = new double[n];

	
	fscanf(inright,"\n",&c);
	for (i = 0; i < n;i++)
	{
	    fscanf(inright,"%lf,",&b[i]);
	}
	fclose(inright);

	strcpy(cMassiv, szStr);
	strcpy(szStr2,basstring);
	strcat(cMassiv, szStr2);

	printf("%s\n",cMassiv);
	
	
	inbas = fopen(cMassiv,"r");
	if (inbas == NULL) {printf("error");  }
	else {printf("open");  }
	if (inright == NULL) {printf("error r"); }
	fscanf(inbas,"%d,%d,%d\n",&Apc,&Aic,&Axc);

	Ap = new int[Apc];
	Ai = new int[Aic];
	Ax = new double[Axc];

	for (i = 0; i < Apc;i++)
	{
	    fscanf(inbas,"%d,",&Ap[i]);
	}
	fscanf(inbas,"\n",&c);
	for (i = 0; i < Aic;i++)
	{
	    fscanf(inbas,"%d,",&Ai[i]);
	}
	fscanf(inbas,"\n",&c);
		
	
	for (i = 0; i < Axc;i++)
	{
	    fscanf(inbas,"%lf,",&Ax[i]);
	}
	fclose(inbas);
 
	
	out = fopen("out.txt","w");
    (void) umfpack_di_symbolic (n, n, Ap, Ai, Ax, &Symbolic, null, null) ;
    (void) umfpack_di_numeric (Ap, Ai, Ax, Symbolic, &Numeric, null, null) ;
    umfpack_di_free_symbolic (&Symbolic) ;
    (void) umfpack_di_solve (UMFPACK_A, Ap, Ai, Ax, x, b, Numeric, null, null) ;
    umfpack_di_free_numeric (&Numeric) ;
    
	printf("Well done");
    for (i = 0 ; i < n ; i++) 
	{
		if (fabs(x[i]) > 0.0000001)
		{
		  if (x[i] > 0)
		  {
			  x[i] *= n;
		      xGross = ceil(x[i]);
		      xKlein = (int) x[i];
			  outx = (xGross-x[i] > x[i] - xKlein)?xKlein:xGross;
			  if (fabs((double) outx - x[i]) > 0.0000001) {printf("ERRROR"); getchar();}
		      else fprintf (out,"%d,%d\n", i, outx);
		  }
		  else
		  {
			  x[i] *= n;
			  x[i] = fabs(x[i]);
		      xGross = ceil(x[i]);
		      xKlein = (int) x[i];
			  outx = (xGross-x[i] > x[i] - xKlein)?(-xKlein):(-xGross);
		      if (fabs((double) outx + x[i]) > 0.0000001) {printf("ERRROR"); getchar();}
		      else fprintf (out,"%d,%d\n", i, outx);		  
		  }
		 }
     }
    fclose(out);
	
}
