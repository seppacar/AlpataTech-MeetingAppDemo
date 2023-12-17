/* eslint-env node */
module.exports = {
    extends: ['eslint:recommended', 'plugin:@typescript-eslint/recommended', 'plugin:@typescript-eslint/stylistic'],
    rules: {
        "@typescript-eslint/no-explicit-any": "off",
    },
    parser: '@typescript-eslint/parser',
    plugins: ['@typescript-eslint'],
    root: true,
};