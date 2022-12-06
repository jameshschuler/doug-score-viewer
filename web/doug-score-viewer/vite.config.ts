import vue from "@vitejs/plugin-vue";
import { resolve } from "path";
import { defineConfig } from "vite";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      assets: resolve(__dirname, "src/assets"),
      "~assets": resolve(__dirname, "/src/assets"),
      "~/": `${resolve(__dirname, "src")}/`,
      "@": resolve("src"),
      "@components": resolve("src/@components"),
    },
  },
  server: {
    port: 3000,
    host: "localhost",
  },
});
