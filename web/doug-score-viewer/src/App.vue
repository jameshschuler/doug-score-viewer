<script setup lang="ts">
import Navbar from "./components/Navbar.vue";

checkAPIHealth();

async function checkAPIHealth() {
  const healthCheckUrl = `${import.meta.env.VITE_API_BASE_URL}/health`;
  const response = await fetch(healthCheckUrl);
  const data = await response.text();
  console.info(`API responded with...${data}`);
}
</script>

<template>
  <Navbar />
  <main class="container is-fluid mt-6">
    <div class="columns">
      <router-view class="column is-12-touch"></router-view>
    </div>
  </main>
  <footer class="footer py-3 mt-5 is-hidden-touch">
    <div class="content has-text-centered">
      <p>Made with <i class="fas fa-fw fa-heart has-text-danger"></i> by <a href="https://jamesschuler.io" target="_blank">James Schuler</a>.</p>
      <p>
        <a href="https://github.com/jameshschuler/doug-score-viewer" target="_blank"><i class="fab fa-fw fa-lg fa-github-alt"></i></a>
      </p>
      <a href="https://www.flaticon.com/free-icons/flag" title="flag icons">Flag icons created by Freepik - Flaticon</a>
    </div>
  </footer>
</template>

<style lang="scss">
.is-red {
  color: #ff0000;

  &:hover {
    color: #ff0000;
  }
}

.has-border-great {
  border-color: hsl(141, 71%, 48%);
}

.has-border-okay {
  border-color: hsl(48, 100%, 67%);
}

.has-border-sad {
  border-color: hsl(348, 100%, 61%);
}

body {
  display: flex;
  flex-direction: column;
  min-height: 100vh;

  #app {
    flex: 1;
    display: flex;
    flex-direction: column;
  }
}
</style>
