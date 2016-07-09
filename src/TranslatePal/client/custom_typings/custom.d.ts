declare var require: Function;

declare interface IIdenticon {
    new(hash: string, size: number): IIdenticon;
    new(hash: string, options: any): IIdenticon;
    toString(): string;
}

// Fix for missing update typing
declare namespace jsSHA {
    export interface jsSHA {
        /**
		 * Takes strString and hashes as many blocks as possible.  Stores the
		 * rest for either a future update or getHash call.
		 *
		 * @expose
		 * @param {string} srcString The string to be hashed
		 */
        update(srcString: string): void;

        getHash(type: string): string;
    }
}