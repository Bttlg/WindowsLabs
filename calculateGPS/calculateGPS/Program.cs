﻿using GPSlibrary;

//Үндсэн программ ажиллуулах хэсэг
calcGPS GPSCHECK = new calcGPS(new byte[]{
           0x40, 0x40,0x67,0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31,0x30,0x36, 0x37,0x36,0x02, 0x20,0x12,0x03,0x16
            ,0x02,0x0d,0x38,0x80,0x80,0x00, 0x12,0x03,0x16,0x02,0x0d,0x3a,0x0f, 0x02,0x55,0x47,0x0a,0xe8,0xfb,0xef,0x16,
            0x02,0x00,0x00,0x00,0x23,0x32,0x05,0x05,0x20,0x01,0x62,0x0c,0x20,0x02,0x0e,0x14,0x0d,0x20,0x01,0x00, 0x0f,0x20,0x01,0x35,
            0x10,0x20,0x02,0xc6,0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xe7,0x33,0x00,0x00,0x8b,0x00,0x02,0x01,0x01,0x01,0x00,0x00,
            0xe0,0x97, 0x9b, 0x01, 0xcb,0x37, 0x03,0x00,0x61,0xaa,0x0d,0x0a
        });
