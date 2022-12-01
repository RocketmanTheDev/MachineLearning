/**
 * The Collatz Tree v1.0
 *
 * Put this file in a directory named CollatzTree and open it with Processing.
 * ... for futher informtion read the full article at 
 * https://www.algoritmarte.com/the-collatz-tree/
 *
 *
 *  Some PARAMETERS to play with ...
 *
 */
float FIRST_BRANCHLEN = 26;   // the length of the first branch of the tree
float START_AEVEN =  4;       // angle increase for even branches (degrees) 
float START_AODD  = -8;       // angle increse for odd branches (degrees)
int BRANCHES = 25;            // number of branches 
float SPEED_AEVEN = 0.4;      // "speed" of even angle (degrees per second)
float SPEED_AODD = -0.3;      // "speed" of odd angle (degrees per second)
int rollcol = 3;              // seconds between the roll of palette
int currscene = 1;            // current scene (used in animation) 0=static 1=forward 2=backward

// ------------------------------------------------------------
/**
 * Other parameters used for the animation
 */

boolean frecord = false;             // set to true if you want to generate the frames
String recorddir = "D:/tmp/video/"; // where the frames are generated (warning they can occupy a lot of space)
int numsaved;
long scenelen[] = {
  //0, 2000, 3000
  //0,  8000, 124000
  0,  8000, 100000
};
// ------------------------------------------------------------

/**
 * "internal" parameters
 */
int FPS = 30; // frames per second
float ae = START_AEVEN * (PI/90); // convert start angle even in radiants
float ao = START_AODD * (PI/90);  // convert start angle odd in radiants
float acce = 0.001; // acceleration even
float acco = 0.001; // acceleration odd
float currspeede = 0;  // speed even 
float currspeedo = 0;  // speed odd
float target_speede = SPEED_AEVEN;
float target_speedo = SPEED_AODD;
float zoom = 1;
float maxy, miny;
float cx, cy;
boolean fsetup = true;

int colray[] = { 
0xff0000ff,0xff2000ff,0xff4000ff,0xff6000ff,
0xff8000ff,0xffA000ff,0xffC000ff,0xffE000ff,
0xffff00E0,0xffff00C0,0xffff00A0,0xffff0080,
0xffff0060,0xffff0040,0xffff6020,0xffff8000,
0xffffA000,0xffffC000,0xffffE000,0xffffff00,

0xffffE000,0xffffC000,0xffffA000,0xffff8000,
0xffff6020,0xffff0040,0xffff0060,0xffff0080,
0xffff00A0,0xffff00C0,0xffff00E0,0xffE000ff,
0xffC000ff,0xffA000ff,0xff8000ff,0xff6000ff,
0xff4000ff,0xff2000ff,
};

// ------------------------------------------------------------
// Surge Verb Pad
int coff;       // value for shifting colors
long tm0 = 0;   // milliseconds at first draw
long maintimer; // current milliseconds
long tmbye;
/**
 * A Collatz number
 */
class TCollatzPoint {
  float branchlen = FIRST_BRANCHLEN;
  float x, y, angle;
  int num;
  public TCollatzPoint( int num, float x, float y, float angle ) {
    this.num = num; this.x = x; this.y = y; this.angle = angle;
  }  
  public TCollatzPoint branch( int num2, float deltaangle ) {    
    float angle2 = angle + deltaangle;
    float x2 = x + branchlen * cos( angle2 ) * zoom;
    float y2 = y + branchlen * sin( angle2 ) * zoom;
    TCollatzPoint res = new TCollatzPoint( num2, x2, y2, angle2 );
    return res;   
  }  
}

/**
 * Initilization stuff
 */
void setup() {
  frameRate( FPS );
  size( 1280, 720, JAVA2D);
  background(0);
  cx = width / 2;
  cy = height / 2;
  if ( frecord ) currscene = 0;
  zoom = 1;
  cy = height-height/10;
  drawCollatz(  ae , ao, BRANCHES );
  drawCollatz(  ae , ao, BRANCHES );
  fsetup = false;
}

/**
 * Draw the Collatz tree
 */
void drawCollatz( float aeven, float aodd, int maxdepth ) {
  TCollatzPoint buf[] = new TCollatzPoint[1024];
  TCollatzPoint buf2[] = new TCollatzPoint[1024];
  TCollatzPoint bufswap[];
  int depth = 0;
  int n = 0, m;
  buf[n++] = new TCollatzPoint( 1, 0, 0, PI / 4 );
  maxy = 0;
  miny = 0;
  strokeWeight( 2 ) ;  
  noFill();  
  while ( depth < maxdepth ) {
    if (currscene == 0) {
      stroke( 0xffffffff ); 
    } else {
      int c = colray[(coff + depth) % colray.length]; 
      //c = (c % 0xffffff) | 0x80000000;
      stroke( c ); 
    }
    m = 0;
    for (int i = 0; i < n; i++) {
      TCollatzPoint p = buf[i];
      int num2 = p.num * 2;
      if ( m >= buf.length - 1 ) break;
      TCollatzPoint p2 = buf[i].branch( num2, aeven );
      if (!fsetup) line( cx + p.x, cy - p.y, cx + p2.x, cy - p2.y );
      if ( p2.y > maxy ) maxy = p2.y;
      if ( p2.y < miny ) miny = p2.y;
      
      buf2[m++] = p2;
      if ( p.num > 1 && (p.num - 1 ) % 3 == 0) {
        num2 = (p.num - 1) / 3;
        p2 = buf[i].branch( num2, aodd );
        if (!fsetup) line( cx + p.x, cy - p.y, cx + p2.x, cy - p2.y );
        if ( p2.y > maxy ) maxy = p2.y;
        if ( p2.y < miny ) miny = p2.y;
        buf2[m++] = p2;
      }      
    }
    n = m;
    bufswap = buf2;
    buf2 = buf;
    buf = bufswap;
    depth++;
  } 
  strokeWeight( 2 ) ;  
  stroke( 0xff00ff00);
  fill(0xffffffff );
  for (int i = 0; i < n; i++) {
    TCollatzPoint p = buf[i];
    if ( ! fsetup) ellipse( cx + p.x, cy - p.y, 7, 7 );
  } 
  float f = (maxy - miny) / height;
  float fy = (maxy + miny) / 2;
  if ( fsetup) {
      zoom += (0.92 - f );
      cy += (height/2 + fy - cy);
  } else {
    if ( currscene == 0 ) {
      //println( "maxy=" + maxy + " miny="+miny + " fy="+fy + " cy="+cy + " f="+f );
      //zoom = 1 / f;
      //cy = height;
    } else {
      zoom += (0.92 - f )/10;
      cy += (height/2 + fy - cy)/100;
    }
  }
}

/**
 * Current elapsed millisecodns (measured differenty if recording)
 */
long elapsedMs() {
  if ( frecord ) {
    return (long) 1000.0*(frameCount-1) /FPS; 
  } else {
    return millis() - tm0;
  }
}

/**
 * Check if we've reached a new scene; if it has changed change the animation parameters
 */
void checkScene() {
  int sc = 0;
  for (int i = 0; i < scenelen.length; i++) {
    if (maintimer > scenelen[i]) sc = i;
  }
  if ( abs( ae ) < 11*PI/180 && abs( ao ) < 11*PI/180 ) sc = 3;
  if (sc != currscene) {
    currscene = sc;
    switch( currscene ) {
      case 1 : 
        break;
      case 2 :
        target_speede = -SPEED_AEVEN;
        target_speedo = -SPEED_AODD;
        break;
      case 3 :
        acce *= 2; acco *= 2;
        tmbye = maintimer + 10000;
        target_speede = 0;
        target_speedo = 0;
        break;
    }
  }
}

/**
 * Very naive speed management
 */
float changeSpeed( float curr, float target, float acc ) {
  if ( curr < target ) {
    curr += acc;
    if ( curr > target ) curr = target;
  }
  if ( curr > target ) {
    curr -= acc;
    if ( curr < target ) curr = target;
  }
  return curr;
}
/**
 * Main draw routine
 */
void draw() {
  maintimer = elapsedMs();
  background(0);
  drawCollatz(  ae , ao, BRANCHES );
  if ( currscene > 0) {
    ae += currspeede * (PI/90) / FPS;
    ao += currspeedo * (PI/90) / FPS;
    currspeede = changeSpeed( currspeede, target_speede, acce );
    currspeedo = changeSpeed( currspeedo, target_speedo, acco );
    
    if ( ( frameCount % (rollcol*FPS) ) == 0 && currscene < 3) coff++;
  }
  if ( frecord ) {
    checkScene();  
    if ( tmbye == 0 || maintimer < tmbye ) {
      numsaved = frameCount;
      saveFrame( recorddir + "vid-######.tga" );      
    } else {
      strokeWeight( 1 );
      stroke(0xffffffff);
      fill(0xffffffff);
      text( "VIDEO ENDED RECORDED FRAMES: " + numsaved, 50, 50 );
    }
  } 
}
