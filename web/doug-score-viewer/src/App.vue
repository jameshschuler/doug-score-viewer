<script setup lang="ts">
import Loading from "./components/Loading.vue";

checkAPIHealth();

async function checkAPIHealth() {
  const healthCheckUrl = `${import.meta.env.VITE_API_BASE_URL}/health`;
  const response = await fetch(healthCheckUrl);
  const data = await response.text();
  console.info(`API responded with...${data}`);
}
</script>

<template>
  <main class="container is-fluid">
    <div class="columns">
      <router-view class="column is-12-touch"></router-view>
    </div>
  </main>
</template>

<style lang="scss"></style>
