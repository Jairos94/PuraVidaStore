import * as CryptoJS from 'crypto-js';


export class EncripDesencrip {
    private static key: string = 'password'

    public static encryptUsingAES256(encoding: string) {
        let _key = CryptoJS.enc.Utf8.parse(this.key);
        let _iv = CryptoJS.enc.Utf8.parse(this.key);
        let encrypted = CryptoJS.AES.encrypt(
            JSON.stringify(encoding), _key, {
            keySize: 16,
            iv: _iv,
            mode: CryptoJS.mode.ECB,
            padding: CryptoJS.pad.Pkcs7
        });
        return encrypted.toString();
    }


    public static decryptUsingAES256(encoding: string) {
        let _key = CryptoJS.enc.Utf8.parse(this.key);
        let _iv = CryptoJS.enc.Utf8.parse(this.key);

        let decrypted = '';
        decrypted = CryptoJS.AES.decrypt(
            encoding, _key, {
            keySize: 16,
            iv: _iv,
            mode: CryptoJS.mode.ECB,
            padding: CryptoJS.pad.Pkcs7
        }).toString(CryptoJS.enc.Utf8);
        return decrypted;
    }
}