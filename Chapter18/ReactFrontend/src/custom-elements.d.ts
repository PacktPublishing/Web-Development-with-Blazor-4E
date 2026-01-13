import 'react';

declare module 'react' {
    namespace JSX {
        interface IntrinsicElements {
            [elemName: string]: any;
        }
    }
}