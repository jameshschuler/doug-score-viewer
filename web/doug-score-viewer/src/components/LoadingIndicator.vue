<template>
  <div class="is-relative outside">
    <span class="vehicle">
      <i class="fa-solid fa-fw fa-3x" :class="randomIcon" :style="randomIconColor"></i>
    </span>
  </div>
</template>
<script setup lang="ts">
import { computed } from "vue";

// yellow, red, orange, blue, black
const iconColor = ["#fbe212", "#de0f18", "#f6ae20", "#afd6e4", "#1c1d21"];
const icons = ["fa-truck-pickup", "fa-car-side", "fa-truck-monster", "fa-van-shuttle"];

const randomIcon = computed(() => {
  const randomIconIndex = Math.floor(Math.random() * icons.length);
  return `${icons[randomIconIndex]}`;
});

const randomIconColor = computed(() => {
  const randomColorIndex = Math.floor(Math.random() * iconColor.length);
  return `color: ${iconColor[randomColorIndex]};`;
});
</script>
<style lang="scss" scoped>
.outside {
  height: 50px;

  .vehicle {
    position: absolute;
    animation: vehicle 3s;
    animation-iteration-count: infinite;
  }
}

@keyframes vehicle {
  from {
    left: 0px;
  }
  to {
    left: 90%;
  }
}
</style>
