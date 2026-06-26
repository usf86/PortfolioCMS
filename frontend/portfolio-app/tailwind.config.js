/** Configuration Tailwind - les couleurs du client sont injectees en CSS variables (voir styles.scss) */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        primary: 'var(--color-primary)',
        secondary: 'var(--color-secondary)'
      }
    }
  },
  plugins: []
};
