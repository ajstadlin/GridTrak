// From Open Solaris Source Code
// Copyright Sun Microsystems

char *itoa(long n, int base)
{
  char	*p;
  int	minus;
  char	buf[36];

  p = &buf[36];
  *--p = '\0';
  if (n < 0) 
  {
    minus = 1;
	n = -n;
  }
  else
  {
    minus = 0;
  }
  if (n == 0)
  {
 	*--p = '0';
  }
  else
  {
	while (n > 0) 
    {
	  *--p = "0123456789abcdef"[n % base];
	  n /= base;
	}
  }
  if (minus)
  {
	*--p = '-';
  }
  return p;
}
